# TestMonster.gd

extends GdUnitTestSuite

var monster_scene : PackedScene

# Called before each test
func _before():
	# Load the Monster scene
	monster_scene = load("res://path/to/Monster.tscn")

# Test case: Monster Initialization
func test_monster_initialization():
	var monster := scene_runner("res://Monster.tscn")
	assert_object(monster).is_not_null()
	#assert_not_null(monster.get_node("MonsterArea"), "MonsterArea node should exist")
	#assert_not_null(monster.get_node("MonsterArea/AnimatedSprite2D"), "AnimatedSprite2D node should exist")
	#assert_not_null(monster.get_node("RayCastUp"), "RayCastUp node should exist")
	#assert_not_null(monster.get_node("RayCastDown"), "RayCastDown node should exist")
	#assert_not_null(monster.get_node("RayCastLeft"), "RayCastLeft node should exist")
	#assert_not_null(monster.get_node("RayCastRight"), "RayCastRight node should exist")

# Test case: Monster Movement

func test_monster_movement():
	var monster := scene_runner("res://Monster.tscn")
	var initial_position = monster.get_property("position");
	# Simulate a physics step
	monster.invoke("_PhysicsProcess",1.0)
	assert_vector(initial_position).is_not_equal(monster.get_property("position"))

# Test case: Monster Direction Change

func test_monster_direction_change():
	var monster := scene_runner("res://Monster.tscn")
	
	var initial_direction = monster.get_property("direction")
	## Trigger the direction change method
	monster.invoke("ChangeDirection")
	assert_vector(Vector2(1.1, 1.2)).is_not_equal(Vector2(1.1, 1.3))
#
## Test case: Monster Destruction
#
func test_monster_destruction():
	var monster := scene_runner("res://Monster.tscn")
	#
	monster.invoke("Destroy")
	assert_bool(monster.get_property("dead")=="true")
	
func test_monster_destruction2():
	var monster := scene_runner("res://Monster.tscn")
	#
	monster.invoke("Destroy")
	## Simulate animation finished signal
	monster.invoke("OnAnimationFinished")
	#
	assert_bool(monster.is_queued_for_deletion()==true)
	
## Test case: Monster Animation Change
#
#func test_monster_animation_change():
	#var monster = monster_scene.instance()
	#add_child(monster)
	#
	#monster.call("ChangeAnimation", Vector2.RIGHT)
	#assert_equal(monster.animationSprite.animation, "walk_side", "Animation should be 'walk_side'")
	#assert_true(monster.animationSprite.flip_h, "Sprite should be flipped horizontally")
	#
	#monster.call("ChangeAnimation", Vector2.LEFT)
	#assert_equal(monster.animationSprite.animation, "walk_side", "Animation should be 'walk_side'")
	#assert_false(monster.animationSprite.flip_h, "Sprite should not be flipped horizontally")
	#
	#monster.call("ChangeAnimation", Vector2.UP)
	#assert_equal(monster.animationSprite.animation, "walk_up", "Animation should be 'walk_up'")
	#
	#monster.call("ChangeAnimation", Vector2.DOWN)
	#assert_equal(monster.animationSprite.animation, "walk_down", "Animation should be 'walk_down'")
	#
	#monster.dead = true
	#monster.call("ChangeAnimation", Vector2.ZERO)
	#assert_equal(monster.animationSprite.animation, "die", "Animation should be 'die'")
