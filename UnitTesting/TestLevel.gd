extends GdUnitTestSuite

#func test_numOfPlayers() -> void:
	#var runner := scene_runner("res://TestLevel.tscn")
	#assert_bool(runner != null)
	#runner.invoke("_Ready")
	#assert_int(runner.get_property("numOfPlayers")==1)
	#
#func test_numOfPlayers2() -> void:
	#var runner := scene_runner("res://TestLevel.tscn")
	#assert_bool(runner != null)
	#runner.invoke("_Ready")
	#runner.invoke("OnPlayerWasRemoved")
	#assert_int(runner.get_property("numOfPlayers")==0)
	#


#
#func test_powerUp() -> void:
	#var runner := scene_runner("res://Box.tscn")
	#assert_bool(runner != null)
	#runner.invoke("setpowerUpChance",100)
	#runner.invoke("collectPowerUps")
	#assert_bool(runner.get_property("hasPowerUp")=="true")
