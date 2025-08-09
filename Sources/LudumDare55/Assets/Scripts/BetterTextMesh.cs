using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BTM_Cutscene
{
	public bool Set;
	public int counter;
	public string Msg;
	public float delay;
	public float typespeed;
	[HideInInspector]
	public float Timer;
	public BTMWave Wave;
	public BTMOutline Outline;
	public BTMColor Color;

	public BTM_Cutscene(string msg, float del, float tSpeed, BTMWave wave, BTMOutline outline, BTMColor col)
	{
		Set = false;
		counter = 0;
		Timer = 0;
		Msg = msg;
		delay = del;
		typespeed = tSpeed;
		Wave = wave;
		Outline = outline;
		Color = col;
	}
}

[System.Serializable]
public class BTMWave
{
	public bool nulled;
	public float Amplitude;
	public float Frequency;
	public float Speed;
	[HideInInspector]
	public float Timer;
	public BTMWave(float amp, float freq, float speed, bool nulld)
	{
		Timer = 0;
		Amplitude = amp;
		Frequency = freq;
		Speed = speed;
		nulled = nulld;
	}

}

[System.Serializable]
public class BTMOutline
{
	public bool nulled;
	public int Count;
	public float Distance;
	public Color Color;
	public BTMOutline(int count, float dist, Color color, bool nulld)
	{
		Count = count;
		Distance = dist;
		Color = color;
		nulled = nulld;

	}
}

[System.Serializable]
public class BTMColor
{
	public bool nulled;
	public Color Color;
	public Gradient Gradient;
	public float GradientFrequency;
	public float GradientSpeed;
	public bool UseGradient;
	[HideInInspector]
	public float Timer;
	public BTMColor(Color color, Gradient grad, float gradFreq, float gradSpeed, bool useGrad, bool nulld)
	{
		Timer = 0;
		Color = color;
		Gradient = grad;
		GradientFrequency = gradFreq;
		GradientSpeed = gradSpeed;
		UseGradient = useGrad;
		nulled = nulld;

	}
}

public class BetterTextMesh : MonoBehaviour {



	[TextArea(5,5)]
	public string text;

	public int fontSize;
	public float deblur;
	public float spacing;
	public Font font;

	public BTMWave Wave;
	public BTMOutline Outline;
	public BTMColor ColorDetails;



	TextMesh[] TMs;
	public GameObject SingleTM;
	public Transform textHolder;

	public BTM_Cutscene CurrentCutscene;

	public bool NoCutscene;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		if (!NoCutscene && CurrentCutscene != null) { //super unoptimized, but it's a game jam so shhh

			if (!CurrentCutscene.Set) {
				if (CurrentCutscene.Wave != null && CurrentCutscene.Wave.nulled) {
					Wave = CurrentCutscene.Wave;
				} else {
					Wave = new BTMWave(0,0,0,true);
				}
				if (CurrentCutscene.Outline != null  && CurrentCutscene.Outline.nulled) {
					Outline = CurrentCutscene.Outline;
				} else {
					Outline = new BTMOutline (32, 0.03f, Color.black,true);
				}
				if (CurrentCutscene.Color != null  && CurrentCutscene.Color.nulled) {
					ColorDetails = CurrentCutscene.Color;
				} else {
					ColorDetails = new BTMColor (Color.white, new Gradient (), 0, 0, false,true);
				}

				CurrentCutscene.Set = true;
			}

			CurrentCutscene.delay -= Time.deltaTime;
			if (CurrentCutscene.delay < 0) {
				CurrentCutscene.Timer -= Time.deltaTime;
				if (CurrentCutscene.Timer < 0) {
					CurrentCutscene.Timer += CurrentCutscene.typespeed;
					CurrentCutscene.counter++;
					if (CurrentCutscene.counter >= CurrentCutscene.Msg.Length) {
						CurrentCutscene.counter = CurrentCutscene.Msg.Length;
					}
					text = CurrentCutscene.Msg.Substring (0, CurrentCutscene.counter);
				}		
			}
		}

		if (VarsUpdated()) {
			CreateLetters ();
			prevText = text;
			prevFontSize = fontSize;
			prevSpacing = spacing;
			prevDeblur = deblur;
			textHolder.localScale = Vector3.one / deblur;
		}

		if (Wave.Amplitude != 0) {
			Wave.Timer += Time.deltaTime;
			for (int i = 0; i < TMs.Length; i++) {
				TMs [i].transform.localPosition = new Vector3 (TMs [i].transform.localPosition.x, (Mathf.Sin (Wave.Frequency * (TMs [i].transform.localPosition.x / deblur) + Wave.Speed*Wave.Timer) * Wave.Amplitude)*deblur, 0);
			}
		}

		if (ColorDetails.UseGradient) {
			ColorDetails.Timer += Time.deltaTime;
			for (int i = 0; i < TMs.Length; i++) {
				float Tval = ((TMs [i].transform.position.x)*ColorDetails.GradientFrequency + ColorDetails.Timer * ColorDetails.GradientSpeed) % 1;
					
				TMs [i].color = ColorDetails.Gradient.Evaluate (Tval);
			}
		}

	}


	void CreateLetters()
	{
		int j = textHolder.childCount-1;
		while (j > -1) {
			Destroy (textHolder.GetChild (j).gameObject);
			j--;
		}

		textHolder.localScale = Vector3.one;
		int chars = text.Length;
		TMs = new TextMesh[chars];
		char[] charArray = text.ToCharArray ();
		float Xpos = 0;
		float Ypos = 0;
		int i = 0;
		j = 0;
		while (i < chars) {

			TextMesh TM = Instantiate(SingleTM,transform.position + new Vector3(Xpos,Ypos,0),transform.rotation,textHolder).AddComponent<TextMesh>();
			TM.text = charArray [i].ToString();
			TM.font = font;
			TM.fontSize = Mathf.RoundToInt(fontSize*deblur);
			CharacterInfo cInf = new CharacterInfo ();
			font.GetCharacterInfo(charArray [i],out cInf, Mathf.RoundToInt(fontSize*deblur));
			Xpos += (cInf.advance*spacing);

			TM.GetComponent<Renderer> ().material = font.material;
			TMs [i] = TM;


			j = 0;
			while(j < Outline.Count)
			{
				TextMesh OTM = Instantiate(SingleTM,Vector3.zero,transform.rotation,TM.transform).AddComponent<TextMesh>();
				OTM.text = charArray [i].ToString();
				OTM.font = font;
				OTM.fontSize = Mathf.RoundToInt(fontSize*deblur);
				OTM.color = Outline.Color;
				OTM.transform.localPosition = new Vector3(Mathf.Cos(((j+0f)/(Outline.Count+0f))*Mathf.PI*2)*(Outline.Distance*deblur),Mathf.Sin(((j+0f)/(Outline.Count+0f))*Mathf.PI*2)*(Outline.Distance*deblur),0.1f);
				OTM.GetComponent<Renderer> ().material = font.material;

				j++;
			}


			i++;
		}

	}




	string prevText;
	int prevFontSize;
	float prevDeblur;
	float prevSpacing;
	BTMWave prevWave;
	BTMOutline prevOutline;
	BTMColor prevColorDetails;

	public bool VarsUpdated()
	{
		bool AllMatches = true;
		if (prevText != text) {AllMatches = false; prevText = text;}
		if (prevFontSize != fontSize) {AllMatches = false; prevFontSize = fontSize;}
		if (prevDeblur != deblur) {AllMatches = false; prevDeblur = deblur;}
		if (prevSpacing != spacing) {AllMatches = false; prevSpacing = spacing;}
		if (prevOutline != null && prevOutline.Color != Outline.Color) {AllMatches = false; prevOutline.Color = Outline.Color;}
		if (prevOutline != null && prevOutline.Count != Outline.Count) {AllMatches = false; prevOutline.Count = Outline.Count;}
		if (prevOutline != null && prevOutline.Distance != Outline.Distance) {AllMatches = false; prevOutline.Distance = Outline.Distance;}

		return !AllMatches;





	}

	public void Clear()
	{
		int j = textHolder.childCount-1;
		while (j > -1) {
			Destroy (textHolder.GetChild (j).gameObject);
			j--;
		}
		TMs = new TextMesh[0];

	}

}
