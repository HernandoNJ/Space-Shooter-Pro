using UnityEngine;

[System.Serializable]
public class Bank
{
    [SerializeField] protected string branchName;
    [SerializeField] protected string location;
    [SerializeField] protected string cashVault;

    public void CheckBalance()
    {
        Debug.Log("Checking balance of: " + branchName);
    }

    public void Withdraw()
    {
        Debug.Log("Withdrawing money from: " + branchName);

    }

    public void Deposit()
    {
        Debug.Log("Depositing money to: " + branchName);
 
    }

}
