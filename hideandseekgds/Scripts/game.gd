extends Control

const GRID_SIZE = 10
const BLANK_CELL : Object = preload("res://Assets/blank.png") 
@onready var color_buttons_container = $HBoxContainer
var color_buttons
var cells = []
var selected_color = null 

func _ready():
	create_grid()
	color_buttons = get_color_buttons()

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

func select_color(color_scene):
	selected_color = color_scene.instance()

func _button_pressed(i: int, j: int) -> void:
	if selected_color:
		place_color(i, j)
	else:
		print("Aucun bateau sélectionné pour être placé.")

func place_color(i: int, j: int):
	if can_place_color(i, j, selected_color.size, selected_color.is_horizontal):
		selected_color.start_position = Vector2(i, j)
		for n in range(selected_color.size):
			var x = i + n if selected_color.is_horizontal else i
			var y = j if selected_color.is_horizontal else j + n
			cells[x][y].icon = load("res://Assets/yellow.png")
		selected_color = null 
	else:
		print("La couleur ne peut pas être placé ici.")

func can_place_color(i: int, j: int, size: int, is_horizontal: bool) -> bool:
	for n in range(size):
		var x = i + n if is_horizontal else i
		var y = j if is_horizontal else j + n
		if x >= GRID_SIZE or y >= GRID_SIZE or cells[x][y].text == "S":
			return false
		if cells[x][y].icon != BLANK_CELL:
			return false
	return true

func _on_button_pressed() -> void:
	selected_color = preload("res://Scenes/Yellow.tscn").instantiate()

func get_color_buttons():
	var buttons = []
	for b in color_buttons_container.get_child_count():
		var child = color_buttons_container.get_child(b)
		if child is Button:
			buttons.append(child)
	return buttons
