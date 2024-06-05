using UnityEngine;
using static Cell;

public class fillingUser : MonoBehaviour
{
    public static fillingUser Instance { private set; get; }
    public bool GameActive { private set; get; }  = true;
    protected Cell _currentFilling;
    private GameUi _gameUi;
    private Timer _timer;
    private int _countError;

    public virtual void Awake()
    {
        Instance = this;
    }

    public virtual void Start()
    {
        _gameUi = GameUi.Instance;
        //_timer = GetComponent<Timer>();
        //_timer.StartTImer();
    }

    public virtual void ChooseCell(Cell cell)
    {
        if (_currentFilling != null)
            CrossChange(_currentFilling.X, _currentFilling.Y, false);

        _currentFilling = cell;
        CrossChange(_currentFilling.X, _currentFilling.Y, true);
    }

    public virtual void PutValue(int value)
    {
        if(_currentFilling != null)
        {
            if (_currentFilling.IsHade)
            {
                if(_currentFilling.Value == value)
                {
                    HOROSHO(value);
                }
                else
                {
                    PLOXA(value);
                }
            }
        }
    }

    public virtual void PLOXA(int value)
    {
        _currentFilling.WrongNumber(value);
        _countError++;
        _gameUi.UpdateErrorText(_countError);
        if (_countError == 3)
        {
            _gameUi.GameOver();
            GameActive = false;
        }
    }

    public virtual void HOROSHO(int value)
    {
        _currentFilling.Active();
        ChooseCell(_currentFilling);
        PutCell.STATICMETHOD(value);
        CellsGrid.Instance.CheckFinished();
    }

    private void CrossChange(int x, int y, bool isChooise)
    {
        CellsGrid.Instance.CrossChange(x, y, isChooise);
    }

    public void ResetGame()
    {
        _countError = 0;
        _gameUi.UpdateErrorText(_countError);
    }
}
