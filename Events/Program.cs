using System;

namespace Events
{
	class Program
	{
		static void Main(string[] args)
		{
			Human h = new Human("John");
			Cat cat = new Cat(h);
			Dog dog = new Dog(h);

			h.GoHome();
			h.CookSomeFood("Paella", 450);
		}
	}

	class Cat
	{
		private Human h;
		private int energy = 20;

		public Cat(Human _h)
		{
			h = _h;

			h.HumanIsHome += AskFood;
			h.FoodIsReady += EatFood;
		}

		private void EatFood(object sender, Food food)
		{
			energy += food.Calories;
			if (sender is Human)
			{
				Human h = (Human)sender;
				Console.WriteLine($"Thank you { h.Name }. This { food.Name } is good. You're not so bad person as I thought.");
			}
		}

		public void AskFood(object sender, EventArgs args)
		{
			if (sender is Human)
			{
				Human h = (Human)sender;
				Console.WriteLine($"Hi { h.Name }. I see that you're tired and whatever. Just give me my food and maybe I'll let you to pet me.");
			}
		}
	}

	class Dog
	{
		private Human h;
		private int energy = 50;

		public Dog(Human _h)
		{
			h = _h;

			h.HumanIsHome += AskFood;
			h.FoodIsReady += EatFood;
		}

		public void EatFood(object sender, Food food)
		{
			energy += food.Calories;
			if (sender is Human)
			{
				Human h = (Human)sender;
				Console.WriteLine($"Thank you { h.Name }! This { food.Name } is fantastic. I will serve you to the rest of my life!");
			}
		}

		public void AskFood(object sender, EventArgs food)
		{
			if (sender is Human)
			{
				Human h = (Human)sender;
				Console.WriteLine($"Hi, { h.Name }! I am your doggie. You're so cool. I don't want to be nice to you, only because I want you to feed me, but please, feel free if you want to.");
			}
		}
	}

	class Human
	{
		public string Name { get; set; }

		public event EventHandler<Food> FoodIsReady; // Why do we need to use event keyword?
		public event EventHandler HumanIsHome;
		
		public Human(string _name)
		{
			Name = _name;
		}

		public void GoHome()
		{
			Console.WriteLine("I'm home!");

			HumanIsHome?.Invoke(this, null);
		}

		public void CookSomeFood(string name, int calories)
		{
			Food food = new Food(name, calories);

			FoodIsReady?.Invoke(this, food);
		}

	}
	class Food : EventArgs
	{
		public string Name { get; set; }

		public int Calories { get; set; }

		public Food(string name, int calories)
		{
			Name = name;
			Calories = calories;
		}
	}
}
