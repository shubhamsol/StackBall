using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICurrency
{
    int Amount { get; }
    string CurrencyCode { get; }
    public void Deposite(int value);
    public void Withdraw(int value);

    public void Save();
    public void Load();
}
