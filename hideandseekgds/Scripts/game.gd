extends Control

const GRID_SIZE = 10
const BLANK_CELL = preload("res://Assets/blank.png") 
@onready var color_buttons_container = $HBoxContainer
@onready var popup_container = $PopupContainer
var color_buttons
var popups
var cells = []
var selected_color = null 

func _ready():
	create_grid()
	color_buttons = get_color_buttons()
	popups = get_Hbox_popup()

func _process(delta: float) -> void:
	rotate_color()

func create_grid():
	for i in range(GRID_SIZE):
		var row = []
		for j in range(GRID_SIZE):
			var cell_button = Button.new()
			cell_button.text = ""
			cell_button.icon = load("res://Assets/blank.png")
			cell_button.pressed.connect(func() -> void:
				_button_pressed(i, j)
			)
			$GridContainer.add_child(cell_button)
			row.append(cell_button)
		cells.append(row)

func _button_pressed(i: int, j: int) -> void:
	if selected_color:
		place_color(i, j)
	else:
		popups[0].visible = true

func place_color(i: int, j: int):
	if can_place_color(i, j, selected_color.size, selected_color.is_vertical):
		selected_color.start_position = Vector2(i, j)
		print(selected_color.color_name)
		for n in range(selected_color.size):
			var x = i + n if selected_color.is_vertical else i
			var y = j if selected_color.is_vertical else j + n
			cells[x][y].icon = load("res://Assets/"+ selected_color.color_name +".png")
		selected_color = null 
	else:
		popups[1].visible = true

func can_place_color(i: int, j: int, size: int, is_vertical: bool) -> bool:
	for n in range(size):
		var x = i + n if is_vertical else i
		var y = j if is_vertical else j + n
		if x >= GRID_SIZE or y >= GRID_SIZE or cells[x][y].text == "S":
			return false
		if cells[x][y].icon != BLANK_CELL:
			return false
	return true

func _on_button_pressed() -> void:
	if color_buttons[0].is_pressed():
		selected_color = preload("res://Scenes/Yellow.tscn").instantiate()
	elif color_buttons[1].is_pressed():
		selected_color = preload("res://Scenes/Green.tscn").instantiate()
	elif color_buttons[2].is_pressed():
		selected_color = preload("res://Scenes/Brown.tscn").instantiate()
	elif color_buttons[3].is_pressed():
		selected_color = preload("res://Scenes/Blue.tscn").instantiate()
	elif color_buttons[4].is_pressed():
		print(popups)

func get_color_buttons():
	var buttons = []
	for b in color_buttons_container.get_child_count():
		var child = color_buttons_container.get_child(b)
		if child is Button:
			buttons.append(child)
	return buttons

func get_Hbox_popup():
	var popup = []
	for l in popup_container.get_child_count():
		var child = popup_container.get_child(l)
		print(child)
		if child is VBoxContainer:
			popup.append(child)
	return popup

func rotate_color():
	if Input.is_action_just_released("rotate"):
		if selected_color != null:
			selected_color.is_vertical = false
			print("ROTATING")
		else:
			popups[2].visible = true
