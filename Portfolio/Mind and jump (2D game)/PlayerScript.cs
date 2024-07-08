using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject CheckerUp;
    public GameObject CheckerDown;
    public GameObject CheckerLeft;
    public GameObject CheckerRight;
    private List<string> buttonSequence = new List<string>();
    public Text sequenceText;
    public Text TimerText;
    public int Timer = 10;
    public bool isTimer = true;
    public Animator animator;
    public GameObject Sprite;
    public Animator BlackScreenAnim;
    public GameObject[] Buttons;
    public bool isBlackScreen;
    public bool isFinish = false;
    public bool isOnce = true;
    public int Clicks;
    public int maxClicks;
    public int PromClicks;
    public GameObject MaximumButtons;

    public bool isPlay = false;
    public GameObject EndUI;
    public int Stars;
    public Image[] StarsImageEnd;
    public Sprite FullStar;
    
    public GameObject PauseMenuButton;

    public GameObject BlackScreenRestart;
    

    public void Start(){
        animator = Sprite.GetComponent<Animator>();
        PromClicks = maxClicks;
        Time.timeScale = 1f;
    }
    public bool CheckPlatfrom(GameObject Checker){
        if(Checker.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Platform"))){
            return true;
        }
        else{
            return false;
        }
    }
    public void InputUp(){
        if(!isTimer && Clicks <= maxClicks){
            Clicks++;
            PromClicks=Clicks;
            buttonSequence.Add("Вверх");
        }
    }
    public void InputLeft(){
        if(!isTimer && Clicks <= maxClicks){
            Clicks++;
            PromClicks=Clicks;
            buttonSequence.Add("Влево");
        }
    }
    public void InputDown(){
        if(!isTimer && Clicks <= maxClicks){
            Clicks++;
            PromClicks=Clicks;
            buttonSequence.Add("Вниз");
        }
    }
    public void InputRight(){
        if(!isTimer && Clicks <= maxClicks){
            Clicks++;
            PromClicks=Clicks;
            buttonSequence.Add("Вправо");
        }
    }

    public void CheckerUpPlatform(){
        if(CheckPlatfrom(CheckerUp)){
            Sprite.transform.position += new Vector3(0f, -1.5f, 0f);
            Player.transform.position = CheckerUp.transform.position;
        }
    }
    public void CheckerDownPlatform(){
        if(CheckPlatfrom(CheckerDown)){
            Sprite.transform.position += new Vector3(0f, 1.5f, 0f);
            Player.transform.position = CheckerDown.transform.position;
        }
    }
    public void CheckerLeftPlatform(){
        if(CheckPlatfrom(CheckerLeft)){
            Sprite.transform.position += new Vector3(1.5f, 0f, 0f);
            Player.transform.position = CheckerLeft.transform.position;
        }
    }
    public void CheckerRightPlatform(){
        if(CheckPlatfrom(CheckerRight)){
            Sprite.transform.position += new Vector3(-1.5f, 0f, 0f);
            Player.transform.position = CheckerRight.transform.position;
        }
    }

    public void PlayButtonSequence(){
        StartCoroutine(PlaySequence());
    }

    private void FixedUpdate()
    {
        UpdateSequenceText();
    }
    private float timer = 0f;
    private int seconds = 10;

    void Update()
    {

        if(isBlackScreen && !isFinish  && !isPlay){
            Buttons[0].SetActive(true);
            Buttons[1].SetActive(true);
            Buttons[2].SetActive(true);
            Buttons[3].SetActive(true);
            Buttons[4].SetActive(true);
            MaximumButtons.SetActive(true);
            PauseMenuButton.SetActive(false);
        }
        else{
            PauseMenuButton.SetActive(true);
            Buttons[0].SetActive(false);
            Buttons[1].SetActive(false);
            Buttons[2].SetActive(false);
            Buttons[3].SetActive(false);
            Buttons[4].SetActive(false);
            MaximumButtons.SetActive(false);
        }
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            seconds--;
            TimerText.text = seconds.ToString();
            timer = 0f;
            if (seconds <= 0)
            {
                string nich = " ";
                TimerText.text = nich.ToString();
                if(!isFinish && isOnce){
                    BlackScreenAnim.Play("BlackScreenAnim");
                    isOnce = false;
                }
                Invoke("AwakeButtons", 1f);
            }
        }
    }
    public void AwakeButtons(){
        isBlackScreen = true;
        isTimer = false;
    }

    private void UpdateSequenceText()
    {
        string sequence = "";
        foreach (string button in buttonSequence)
        {
            sequence += button + " ";
        }
        sequenceText.text = sequence;

        // Сдвигаем текст влево, если его длина превышает ширину экрана
        //if (sequenceText.preferredWidth > Screen.width)
        //{
        //    sequenceText.t1ransform.globalScale -= new Vector3(0.1f, 0.1f, 0f);
        //}
    }
    private IEnumerator PlaySequence(){
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if(buttonSequence != null){
            isPlay = true;
            PauseMenuButton.SetActive(true);
            Buttons[0].SetActive(false);
            Buttons[1].SetActive(false);
            Buttons[2].SetActive(false);
            Buttons[3].SetActive(false);
            Buttons[4].SetActive(false);
            MaximumButtons.SetActive(false);
            isTimer = true;
            List<string> sequenceCopy = new List<string>(buttonSequence);
            foreach (string button in sequenceCopy){
                switch (button){
                    case "Вверх":
                        if(CheckPlatfrom(CheckerUp)){
                            animator.Play("PlayRunUp");
                            PromClicks--;
                        }
                        yield return new WaitForSeconds(1f);
                        animator.Play("PlayerIdleAnimation", 0, normalizedTime);
                        CheckerUpPlatform();
                        yield return new WaitForSeconds(0.1f);
                        break;
                    case "Вниз":
                        if(CheckPlatfrom(CheckerDown)){
                            animator.Play("PlayRunDown");
                            PromClicks--;
                        }
                        yield return new WaitForSeconds(1f);
                        animator.Play("PlayerIdleAnimation", 0, normalizedTime);
                        CheckerDownPlatform();
                        yield return new WaitForSeconds(0.1f);
                        break;
                    case "Влево":
                        if(CheckPlatfrom(CheckerLeft)){
                            animator.Play("PlayRunLeft");
                            PromClicks--;
                        }
                        yield return new WaitForSeconds(1f);
                        animator.Play("PlayerIdleAnimation", 0, normalizedTime);
                        CheckerLeftPlatform();
                        yield return new WaitForSeconds(0.1f);
                        break;
                    case "Вправо":
                        if(CheckPlatfrom(CheckerRight)){
                            animator.Play("PlayRunRight");
                            PromClicks--;
                        }
                        yield return new WaitForSeconds(1f);
                        animator.Play("PlayerIdleAnimation", 0, normalizedTime);
                        CheckerRightPlatform();
                        yield return new WaitForSeconds(0.1f);
                        break;
                }
                buttonSequence.RemoveAt(0);
            }
            isTimer = false;
        }
        isTimer = false;
        if(!isFinish){
            BlackScreenAnim.Play("UnBlackScreenAnim");
            yield return new WaitForSeconds(3f);
            RestartLevel();
        }
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("End")){
            Debug.Log("FINISH!");
            PauseMenuButton.SetActive(false);
            Buttons[0].SetActive(false);
            Buttons[1].SetActive(false);
            Buttons[2].SetActive(false);
            Buttons[3].SetActive(false);
            Buttons[4].SetActive(false);
            MaximumButtons.SetActive(false);
            BlackScreenAnim.Play("UnBlackScreenAnim");
            isFinish = true;
            animator.Play("PlayRunRight");
            if(Stars == 3){
                StarsImageEnd[0].sprite = FullStar;
                StarsImageEnd[1].sprite = FullStar;
                StarsImageEnd[2].sprite = FullStar;
            }
            else if(Stars == 2){
                StarsImageEnd[0].sprite = FullStar;
                StarsImageEnd[1].sprite = FullStar;
            }
            else if(Stars == 1){
                StarsImageEnd[0].sprite = FullStar;
            }
            EndUI.SetActive(true);
            StartCoroutine(WhyNot());
        }
        if(col.gameObject.CompareTag("Star")){
            Stars++;
            Destroy(col.gameObject);
        }
    }
    public IEnumerator WhyNot(){
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }

    public IEnumerator Wait(){
        Debug.Log("Wait");
        yield return new WaitForSeconds(3f);
        Debug.Log("NewScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("YEAH");
    }
    public void RestartLevel(){
        //Появление чёрного экрана и исчезновение др объектов
        BlackScreenRestart.SetActive(true);
        //Player.SetActive(false);
        PauseMenuButton.SetActive(false);
        StartCoroutine(Wait());
    }
    public void CloseHud(){
        //Player.SetActive(false);
        PauseMenuButton.SetActive(false);
        EndUI.SetActive(false);
    }
}