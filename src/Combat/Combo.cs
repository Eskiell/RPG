namespace RPG.Combat;

/// <summary>
/// Sistema de combo: acumula pressionamentos de tecla em um intervalo de tempo e dispara um ataque ao atingir o mínimo.
/// </summary>
public class Combo
{
    private readonly Action _attackAction;
    private readonly int _maxComboTime;
    private readonly int _minKeyPressCount;
    private DateTime _comboStartTime;
    private int _currentKeyPressCount;

    /// <summary>
    /// Inicializa o sistema de combo.
    /// </summary>
    /// <param name="maxComboTime">Tempo máximo em ms entre pressionamentos antes de resetar.</param>
    /// <param name="minKeyPressCount">Número mínimo de pressionamentos para disparar o ataque.</param>
    /// <param name="attackAction">Ação executada ao completar o combo.</param>
    public Combo(int maxComboTime, int minKeyPressCount, Action attackAction)
    {
        _maxComboTime = maxComboTime;
        _minKeyPressCount = minKeyPressCount;
        _attackAction = attackAction;
        _currentKeyPressCount = 0;
        _comboStartTime = DateTime.MinValue;
    }

    /// <summary>
    /// Registra um pressionamento de tecla. Reseta o combo se expirado; dispara o ataque se o mínimo for atingido.
    /// </summary>
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

    /// <summary>
    /// Verifica se o tempo limite entre pressionamentos foi excedido.
    /// </summary>
    /// <returns><c>true</c> se o combo expirou; caso contrário, <c>false</c>.</returns>
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
