using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public static GameUi Instance {  get; private set; }
    [SerializeField] private GameObject _windowGameOver;
    [SerializeField] private GameObject _windowWin;
    [SerializeField] private GameObject _windowPause;
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _finishTime;
    [SerializeField] private Text _countError;
    private fillingUser _filling;
    private Timer _timer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        _timer = Timer.Instance;
        _filling = fillingUser.Instance;
    }

    public void UpdateErrorText(int countError)
    {
        _countError.text = $"Ошибки {countError}/3";
    }

    public void GameOver()
    {
        _timer.StopTimer();
        _animator.SetTrigger("GameOver");
    }

    public virtual void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueGame()
    {
        _windowGameOver.SetActive(false);
        _filling.ResetGame();
    }

    public virtual void Pause(bool isActive)
    {
        _windowPause.SetActive(isActive);
        Time.timeScale = isActive == true ? 0 : 1;
    }

    public void Win()
    {
        _animator.SetTrigger("Win");
        _timer.OutputTimeInText(_finishTime);
    }

    public virtual void ExitMenu()
    {
        SceneManager.LoadScene(0);
    }

}
