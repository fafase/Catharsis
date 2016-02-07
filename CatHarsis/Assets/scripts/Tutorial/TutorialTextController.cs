using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_5_3
using UnityEngine.SceneManager;
#endif

public class TutorialTextController : MonoBehaviour {

	[SerializeField] private Text textCat0 = null;
	[SerializeField] private Text textCat1 = null;
	[SerializeField] private Text textJelly0 = null;
	[SerializeField] private Text textJelly1 = null;
	[SerializeField] private Animator animator = null;
	[SerializeField] private InputManager inputManager = null; 
	[SerializeField] private TutorialCatController tutCatCtrl = null;
	[SerializeField] private GameObject tapText = null;
	[SerializeField] private GameObject jellyFish = null;
	[SerializeField] private GameObject movementTutorial = null;
	[SerializeField] private FadeController fadeCtrl = null;
	[SerializeField] private CameraSmoothFollow cameraFollow = null;

	private int index = 0;

	private void Awake()
	{
		this.tapText.SetActive (false);
		this.jellyFish.SetActive (false);
		this.movementTutorial.SetActive (false);
		this.fadeCtrl.StartFade ("FadeIn", null);
		this.cameraFollow.enabled = false;
	}

	public void WaitForTap()
	{
		this.inputManager.OnSingleTap += SetNextAnim;
		this.tapText.SetActive (true);
	}
	public void WaitForFinalTap()
	{
		this.inputManager.OnSingleTap += LoadLevel;
		this.tapText.SetActive (true);
	}
	private void LoadLevel(Vector3 vec)
	{
		Application.LoadLevel ("Level1");
	}
	private void SetNextAnim(Vector3 vec)
	{
		this.inputManager.OnSingleTap -= SetNextAnim;
		index++;
		this.animator.SetInteger ("index", index);
		this.tapText.SetActive (false);
	}

	public void WaitForTapIdle()
	{
		this.inputManager.OnSingleTap += SetIdle;
		this.cameraFollow.enabled = true;
	}

	private int tapCounter = 0;
	private void SetIdle(Vector3 vec)
	{
		this.movementTutorial.SetActive (false);
		this.animator.SetBool ("idle", true);
		this.tapText.SetActive (false);
		if (++this.tapCounter == 10) 
		{
			UnregisterMovement();
			UnregisterJump();
			this.inputManager.OnSingleTap -= SetIdle;
			this.tutCatCtrl.MoveToPosition(()=> {
				this.animator.SetBool ("idle", false);
				index++;
				this.animator.SetInteger ("index", index); 
				this.cameraFollow.enabled = false;
			});
		}
	}

	public void Anim0Text()
	{
		this.textCat0.text = "Mmm...Where am I?";
	}

	public void Anim1Text()
	{
		this.textJelly0.text = "You are finally deceased.";
		this.textCat0.text = "Deceased??!! You mean deceased of death?";
		this.textJelly1.text = "Well...Somehow...yeah";
		this.textCat1.text = "Who's there?";
	}

	public void Anim2Text()
	{
		StartCoroutine (ShowJelly ());
		this.textCat0.text = "Oh!? There you are. But where am I?";
		this.textJelly0.text = "In limbo";
		this.textCat1.text = "What for?";
		this.textJelly1.text = "So that you can overcome challenges and get back to life.";
	}

	public void Anim3Text()
	{
		this.textCat0.text = "I'm 20 year-old cat, I could barely move. I ain't wanna go back.";
		this.textJelly0.text = "It's the game. You just do.";
		this.textCat1.text = "Arthritis, painful moves, not aware that I'm pissing myself...mmm";
		this.textJelly1.text = "Just-do-the freaking game-already.";
	}

	public void Anim4Text()
	{
		this.textJelly0.text = "I am your soul.";
		this.textCat0.text = "??!!...And you look like a big-eye jelly fish. Right...";
		this.textJelly1.text = "You prefer Maggie Gyllenhal?";
		this.textCat1.text = "Touché...";
	}

	public void Anim5Text()
	{
		this.textJelly0.text = "Let's have a walk.";
		this.textCat0.text = "Don't think I can.";
		this.textJelly1.text = "Now you can. If the player would not mind a little tap.";
		this.textCat1.text = "Player...? Can you do that?";
	}

	public void Anim6Text()
	{
		this.tutCatCtrl.HandleSingleTap (new Vector3(0.5f,-5.9f,0f));
		this.textJelly0.text = "Ok, come back here now.";
		this.textCat0.text = "So in this game of death, I can walk to overcome challenges. What if I fail?";
		this.textJelly1.text = "You try again.";
		this.textCat1.text = "Mmm...Original.";
	}
	public void Anim7Text()
	{
		this.textJelly0.text = "Let's see what we can do shall we?";
		this.textCat0.text = "Sure thing!";
	}

	private IEnumerator ShowJelly()
	{
		this.jellyFish.SetActive (true);
		SpriteRenderer sp = this.jellyFish.GetComponent<SpriteRenderer> ();
		sp.color = new Color (1f,1f,1f,0f);
		while (sp.color.a < 1f) 
		{
			Color col = sp.color;
			col.a += Time.deltaTime;
			sp.color = col;
			yield return null;
		}
	}

	public void RegisterMovement()
	{
		this.movementTutorial.SetActive (true);
		this.tutCatCtrl.RegisterMovement ();
	}

	public void RegisterJump()
	{
		this.tutCatCtrl.RegisterJump ();
	}

	public void UnregisterMovement()
	{
		this.tutCatCtrl.UnregisterMovement ();
	}
	
	public void UnregisterJump()
	{
		this.tutCatCtrl.UnregisterJump ();
	}

	public void SkipTutorial()
	{

#if UNITY_5_2
		this.fadeCtrl.StartFade("FadeOut",()=>{Application.LoadLevel("LevelMap");});
#endif
#if UNITY_5_3
		this.fadeCtrl.StartFade("FadeOut",()=>{SceneManager.LoadScene("LevelMap");});
#endif
	}
}

public class MyClass{
	public int TestMethod(int a , int b){ return a + b;}
}
