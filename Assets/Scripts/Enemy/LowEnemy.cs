
using UnityEngine;

public abstract class LowEnemy : Enemy
{
    private EIdleState idleState;
    private ERunState runState;
    private ENormalATKState normalATKState;

    protected void InitState()
    {
        idleState = new EIdleState(this);
        runState = new ERunState(this);
        normalATKState = new ENormalATKState(this);
        this.GetStateManager().ChangeState(runState);
    }
    public void ResetHealth()
    {
        _currentHealth = _maxHealth; // Reset health to max
        // Reset any other state needed
    }
    public abstract void NormalATK();

    public EIdleState GetIdleState()
    {
        return idleState;
    }
    public ERunState GetRunState()
    {
        return runState;
    }
    public ENormalATKState GetNormalATKState()
    {
        return normalATKState;
    }

}
