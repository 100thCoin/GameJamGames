using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour {

	public DataHolder Main;
	public TextMesh TM;
	public SpriteRenderer SR;
	public Sprite Blank;
	public Sprite Grid1;
	public Sprite Grid2;
	public Sprite Miggs1;
	public Sprite Miggs2;
	public Sprite MiggsPM1;
	public Sprite MiggsPM2;



	// Use this for initialization
	void OnEnable () {

		string Note = "Current Objective: \n";
		SR.sprite = Blank;
		// ~ the sactuary ~

		//You pop out of a flower

		//Tilde rushes over, and welcomes you to the sactuary. The last place on earth with life.

		//She hands you a bulb and tells you to place it anywhere you like.


		//if the player takes over 7 seconds to place it, she says it's a pretty hefty commitment, this will be where you start your garden, so choose a good spot.


		//if the player waits longer, she finds a spot and suggests it for you.

		// nicely done! (if you pick that spot)
		// That's a great spot too! (if you don't lol)


		//she hands you some seeds and walks you through the breeding process.

		//you grow a blue flower. Great work! now Stand back... (poof) wow

		// Now you can harvest it for more seeds.

		// You've got tha hang of it already! I have my own garden to tend to, so if you need me I'll be over here.

		// oh, and just one more thing.

		// one teensy little thing.

		// DO NOT PLANT A FLOWER OUTSIDE OF YOUR GARDEN.

		// Well, carry on now.



		// CURRENT OBJECTIVE: 


		//If you run back to tilde she'll explain tihngs more.


		if(Main.Progress >= 3)
		{
			Note = "Current Objective: \n\nCombine more than 2\nseeds.\nGrow a yellow flower\nand show Tilde.";
		}

		// Meet Migfrir cutscene.

		if(Main.Progress >= 3.1f)
		{
			Note = "Current Objective: \n\nFight the bug!";
		}

		// Miggs tells you to stop on by after showing Tilde your work. 3.15

		// Talking to miggs, she says Tilde is a bit creepy, but she'll appreciate your flower. 3.16


		if(Main.Progress >= 3.14f)
		{
			Note = "Current Objective: \n\nTalk to Tilde.";
		}

		//Tilde cutscene. Great work! Be good, young one.  3.3

		if(Main.Progress >= 3.3f)
		{
			Note = "Current Objective: \n\nGrow a green flower.";
		}

		// Talk to Tilde. She doesn't help you.  3.31

		// she still doesn't help you  3.32

		//talk to miggs lol. She tells you to make a small loop, and offeres a drawing.  3.33

		if(Main.Progress >= 3.33f)
		{
			Note = "Current Objective: \n\nGrow a green flower.";
			SR.sprite = Grid1;
		}

		//miggs says she helped you enough, you got it from here. (though colors a little more on the drawing) 3.34

		if(Main.Progress >= 3.34f)
		{
			Note = "Current Objective: \n\nGrow a green flower.";
			SR.sprite = Grid2;
		}


		if(Main.Progress >= 3.4f)
		{
			Note = "Current Objective: \n\nShow Tilde your work.";
			SR.sprite = Blank;

		}

		//Miggs: Heyy, you got it? nice! 3.41

		//miggs, well, go show tilde. 3.42


		//Tilde follows you back to see the flower, and it's under attack. beat up a bug!

		//nice.

		//Tilde cutscene, she says you're learning a lot! Keep tending to your garden.

		if(Main.Progress >= 3.5f)
		{
			Note = "Current Objective: \n\nTalk to Miggs.";
		}

		//tilde only says keep tending to your work.

		//Miggs: yo, you need some seeds? I can help you out.

			//miggs tells you to place some flowers in the darkness... so long as Tilde isn't around.

		//Check it out... the flowers wilt, and leave behind ash.

		//but the ash is a very potent fertilizer! Just be sure you don't plant your fertilized plants in the light.

		//Bam. loads of seeds. Go ahead, try it! 

		if(Main.Progress >= 3.6f)
		{
			Note = "Current Objective: \n\nPlant a flower in the\ndark.";
		}

		//tilde says she's busy, talk to her later.

		//Miggs reassures you it's okay. 3.61

		//you grow a dark plant and fight off its bug. Loadsa seeds.

		if(Main.Progress >= 3.7f)
		{
			Note = "Current Objective: \n\nTalk to Miggs.";
		}

		//tilde says she's busy, talk to her later.

		//Miggs says "Keep it up."

		if(Main.Progress >= 3.8f)
		{
			Note = "Current Objective: \n\nTalk to Miggs.";
		}

		//tilde says she's busy, talk to her later.

		//Miggs says Oh nice! She's just finished setting up a small table for decoration... could you make her one of these? but no peeking! it's a surprise.

		if(Main.Progress >= 3.9f)
		{
			Note = ""; //Miggs doodles
			SR.sprite = Miggs1;
				
		}

		//tilde says she's alsmost done, talk to her later.

		//Miggs helps you out and circles the first seed to plant. 3.91

		if(Main.Progress >= 3.91)
		{
			SR.sprite = Miggs2;
		}

		//Miggs says, you got this.

		//Macaroni scene! woo hoo.

		//Tilde shows up... Miggs's garden is full of dark plants. oh no.

		//Tilde fights miggs.

		//Tilde tells you to wait in your garden.

		if(Main.Progress >= 4f)
		{
			SR.sprite = Blank;

			Note = "Current Objective: \n\nWait...";
		}

		//tilde walks up to you. She says she prays you weren't influenced. Its okay now, you are safe. It' just the two of you now.

		if(Main.Progress >= 4.1f)
		{
			Note = "Current Objective: \n\nTalk to Miggs.";
		}

		//You talk to miggs, but there is no response. she dead, yo.

		if(Main.Progress >= 4.2f)
		{
			Note = "Current Objective: \n\nTalk to Tilde.";
		}

		//You talk to Tilde. She assures you it's safe. Can you grow her a white flower?
		// you make a white flower by combining all the colors of flowers


		//tilde asks how the white flower is coming along?

		//Talkin to miggs: there still isn't a response.

		if(Main.Progress >= 4.3f)
		{
			Note = ""; //Miggs note.
			SR.sprite = MiggsPM1;

		}

		//You shoulkdn't leave your garden just yet.


		if(Main.Progress >= 4.4f)
		{
			SR.sprite = Blank;

			Note = "Current Objective: \n\nKill Tilde.";
		}

		//Tilde is glad to see you. Did you bring the white flo- (you plant the deathbulb)

		//her garden withers away.

		//you fight

		//you win.

		//ash turns to passion.

		//you shouldn't leave the sanctuary... yet.

		if(Main.Progress >= 4.5f)
		{
			Note = ""; // miggs note
			SR.sprite = MiggsPM2;

		}


		//grow a lifebulb.

		//You should find a better place to plant it.

		if(Main.Progress >= 4.6f)
		{
			SR.sprite = Blank;

			Note = "Current Objective: \n\nLeave the sanctuary";
		}

	
		//win

		TM.text = Note;


	}

}
