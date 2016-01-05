using UnityEngine;
using System.Collections;

public class AFlyingNames {

	System.Random _randomGen;

	private string[] _flyingNames = new string[]{
		"BabyDuck.obj",
		"Black_Sheep.obj",
		"Blue_Frog.obj",
		"Brown_Rabbit.obj",
		"Bull.obj",
		"Capybara.obj",
		"Cat.obj",
		"Chicken.obj",
		"Cow.obj",
		"Dog.obj",
		"Doge.obj",
		"Doges_Cousin.obj",
		"Duck.obj",
		"Electric.obj",
		"Fuzz.obj",
		"Ganster.obj",
		"Goat.obj",
		"Gray_Rabbit.obj",
		"GreenFrog.obj",
		"Hipster_Whale.obj",
		"Horse.obj",
		"Kiwi.obj",
		"Omnious_Cat.obj",
		"Penguin.obj",
		"Pew_Pew_Pug.obj",
		"Pig.obj",
		"Pigeon.obj",
		"Piggy_Bank.obj",
		"Pink_Rabbit.obj",
		"Purple_Frog.obj",
		"Red_Frog.obj",
		"Sheep.obj",
		"Snail.obj",
		"Tabby_Cat.obj",
		"Taxi.obj",
		"Tortoise.obj",
		"Truck.obj",
		"Unihorse.obj",
		"Wagon.obj",
		"Wizard.obj",
		"Wolf.obj",
		"bat_3.obj",
		"bird_idle_01.obj",
		"missile_truck.obj",
		"muscle.obj",
		"oil_truck.obj",
		"snowman.obj",
		"truck_blue.obj",
		"truck_green.obj",
		"ute.obj",
		"xi.obj",
	};

	public string getRandomFlyingName() {
		if (_randomGen == null) {
			_randomGen = new System.Random (Time.time.ToString ().GetHashCode ());
		}
		string name = _flyingNames [_randomGen.Next (0, _flyingNames.GetLength (0) - 1)];
		return name.Substring(0,name.Length-4);
	}
}
