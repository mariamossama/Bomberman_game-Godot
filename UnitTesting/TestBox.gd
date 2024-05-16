extends GdUnitTestSuite

func test_destroy() -> void:
	var runner := scene_runner("res://Box.tscn")
	assert_bool(runner != null)
	runner.invoke("Destroy")
	assert_bool(runner.get_property("isDestroyed")=="false")


func test_powerUp() -> void:
	var runner := scene_runner("res://Box.tscn")
	assert_bool(runner != null)
	runner.invoke("setpowerUpChance",100)
	runner.invoke("collectPowerUps")
	assert_bool(runner.get_property("hasPowerUp")=="true")
