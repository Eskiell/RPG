namespace RPG.Core;

public class Combo
{
    private readonly int _maxComboTime;
    private readonly int _minKeyPressCount;
    private readonly Action _attackAction;

    private int _currentKeyPressCount;
    private DateTime _comboStartTime;

    public Combo(int maxComboTime, int minKeyPressCount, Action attackAction)
    {
        _maxComboTime = maxComboTime;
        _minKeyPressCount = minKeyPressCount;
        _attackAction = attackAction;

        _currentKeyPressCount = 0;
        _comboStartTime = DateTime.MinValue;
    }

    public void RegisterKeyPress()
    {
        if (IsExpired()) ResetCombo();

        _currentKeyPressCount++;
        _comboStartTime = DateTime.Now;

        if (_currentKeyPressCount >= _minKeyPressCount)
        {
            ExecuteAttack();
            ResetCombo();
        }
    }

    public bool IsExpired()
    {
        var comboElapsedTime = DateTime.Now - _comboStartTime;
        return comboElapsedTime.TotalMilliseconds > _maxComboTime;
    }

    private void ExecuteAttack()
    {
        _attackAction?.Invoke();
    }

    private void ResetCombo()
    {
        _currentKeyPressCount = 0;
        _comboStartTime = DateTime.MinValue;
    }
}