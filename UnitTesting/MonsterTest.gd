extends GdUnitTestSuite

func test_destroy() -> void:
	var runner := scene_runner("res://Monster.tscn")
	assert_bool(runner != null)
	runner.invoke("Destroy")
	assert_bool(runner.get_property("dead")=="true")

