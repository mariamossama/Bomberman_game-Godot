# TestBox.gd

extends GdUnitTestSuite


func test_box_initialization():
	var runner := scene_runner("res://Box.tscn")
	assert_bool(runner != null)

func child_nodes():
	var runner := scene_runner("res://Box.tscn")
	var animation:Node = runner.find_child("BoxAnimation")
	var collision:Node = runner.find_child("CollisionShape2")
	assert_object(animation).is_not_null()
	assert_object(null).is_not_null()


## Test case: Box Destruction by Flame
func test_box_destruction_by_flame():
	var runner := scene_runner("res://Box.tscn")
	var runner2 := scene_runner("res://ExplosionFlame.tscn")
	runner.invoke("OnBodyEntered", runner2)
	assert_bool(runner.get_property("isDestroyed")=="true")
	
func test_power_up_spawning():
	var box := scene_runner("res://Box.tscn")
	box.invoke("setpowerUpChance", 100) # Set 100% chance for power-up to spawn
	box.invoke("Destroy")
	assert_bool(box.get_property("hasPowerUp")=="true")
	#assert_true(power_up_exists, "Power-up should be present in the scene tree")
	
	
func test_power_up_spawning2():
	var box := scene_runner("res://Box.tscn")
	var power_up_exists = false
	for child in get_tree().root.get_children():
		if child is Area2D and child.name == "res://Asset/PowerUp.tscn":
			power_up_exists = true
			break
	assert_bool(power_up_exists==true)
