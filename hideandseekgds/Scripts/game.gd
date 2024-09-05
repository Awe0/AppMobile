extends Control

const GRID_SIZE = 10
const BLANK_CELL = preload("res://Assets/blank.png") 
@onready var texture_rect_preview: TextureRect = $TextureRect
var cells = []
var selected_color = null 
var color_scenes = [
	preload("res://Scenes/Yellow.tscn"),
	preload("res://Scenes/Green.tscn"),
	preload("res://Scenes/Brown.tscn"),
	preload("res://Scenes/Blue.tscn")
]
var color_previews = {
	"yellow": preload("res://Assets/yellow_preview.png"),
	"green": preload("res://Assets/green_preview.png"),
	"brown": preload("res://Assets/brown_preview.png"),
	"blue": preload("res://Assets/blue_preview.png")
}

func _ready():
	create_grid()
	assign_random_color()

func _process(delta: float) -> void:
	rotate_color()

func create_grid():
	for i in range(GRID_SIZE):
		var row = []
		for j in range(GRID_SIZE):
			var cell_button = Button.new()
			cell_button.icon = load("res://Assets/blank.png")
			cell_button.pressed.connect(func() -> void:
				_button_pressed(i, j)
			)
			$GridContainer.add_child(cell_button)
			row.append(cell_button)
		cells.append(row)

func _button_pressed(i: int, j: int) -> void:
	var text_error = "ERROR"
	if selected_color:
		place_color(i, j)
		# Vérifier s'il reste de la place pour la pièce actuelle
		if not can_place_anywhere(selected_color):
			print("Aucune place disponible pour la pièce actuelle")
			# Ajouter une action, comme afficher un popup ou un message ici
		else:
			# Générer une nouvelle couleur si la pièce actuelle ne peut plus être placée
			assign_random_color()
	else:
		SignalBus.popup_displayed.emit(text_error)
		#popup_container.add_child(popup_instance)

func place_color(i: int, j: int):
	var text_error = "Can't place here."
	if can_place_color(i, j, selected_color.size, selected_color.is_vertical):
		selected_color.start_position = Vector2(i, j)
		for n in range(selected_color.size):
			var x = i + n if selected_color.is_vertical else i
			var y = j if selected_color.is_vertical else j + n
			cells[x][y].icon = load("res://Assets/"+ selected_color.color_name +".png")
		assign_random_color()
	else:
		SignalBus.popup_displayed.emit(text_error)
		#popup_container.add_child(popup_instance)

func can_place_color(i: int, j: int, size: int, is_vertical: bool) -> bool:
	for n in range(size):
		var x = i + n if is_vertical else i
		var y = j if is_vertical else j + n
		if x >= GRID_SIZE or y >= GRID_SIZE or cells[x][y].text == "S":
			return false
		if cells[x][y].icon != BLANK_CELL:
			return false
	return true

func rotate_color():
	var text_error = "No color selected, you can't rotate."
	if Input.is_action_just_released("rotate"):
		if selected_color != null:
			if selected_color.is_vertical == true:
				selected_color.is_vertical = false
				texture_rect_preview.rotation = -1.57079994678497
			else:
				selected_color.is_vertical = true
				texture_rect_preview.rotation = 0
	else:
		SignalBus.popup_displayed.emit(text_error)
		#popup_container.add_child(popup_instance)

func assign_random_color():
	texture_rect_preview.rotation = 0
	var random_index = randi() % color_scenes.size()
	selected_color = color_scenes[random_index].instantiate()
	var color_name = selected_color.color_name
	if color_previews.has(color_name):
		texture_rect_preview.texture = color_previews[color_name]

func can_place_anywhere(piece) -> bool:
	for i in range(GRID_SIZE):
		for j in range(GRID_SIZE):
			if can_place_color(i, j, piece.size, true):
				return true
			if can_place_color(i, j, piece.size, false):
				return true
	return false
