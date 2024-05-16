extends GdUnitTestSuite

func test_flicker() -> void:
	var runner := scene_runner("res://Asset//PowerUp.tscn")
	assert_bool(runner != null)
	runner.invoke("TurnOnFlickerCloseToExpiration")
	assert_bool(runner.get_property("_isShouldFlicker")=="true")
	
func test_invicibility() -> void:
	var runner := scene_runner("res://Asset//PowerUp.tscn")
	assert_bool(runner != null)
	runner.invoke("RemoveInvincibility")
	assert_bool(runner.get_property("_isInvincible")=="false")


		
