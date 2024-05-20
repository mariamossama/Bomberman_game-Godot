extends GdUnitTestSuite

# Test Case 1: Initial conditions
func test_initial_conditions() -> void:
	var player := scene_runner("res://Player.tscn")
	assert_bool(player != null) 
	assert_bool(player.get_property("nrOfRangeIncreasePowerUps")=="0")



# Test Case 2: Applying power-up increases player's range
func test_apply_power_up_increases_range() -> void:
	var runner := scene_runner("res://Asset/FireRangeIncreasePowerUp.tscn")
	assert_bool(runner != null) 
	var player := scene_runner("res://Player.tscn")
	#var spell:Node = runner.find_child("FireRangeIncreasePowerUp")
	runner.invoke("ApplyPowerUp",player)
	assert_bool(player.get_property("nrOfRangeIncreasePowerUps")=="1")
#
