using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftDatabase : MonoBehaviour
{
    [SerializeField] private List<CraftRecipeSO> recipesSODatabase = new();
    public List<CraftRecipeSO> Recipes => recipesSODatabase;

    public Dictionary<CraftRecipeSO, bool> UnlockedRecipes { get; } = new();

    private void Start()
    {
        BuildDatabase();
    }

    private void BuildDatabase()
    {
        foreach (var recipe in recipesSODatabase)
        {
            UnlockedRecipes.TryAdd(recipe, false);
        }
    }

    public CraftRecipeSO GetRecipeSO(ItemSO result)
    {
        return recipesSODatabase.Find(recipe => recipe.ItemResult == result);
    }

    public CraftRecipeSO GetRecipeSO(int id)
    {
        return recipesSODatabase.Find(recipe => recipe.ItemResult.Id == id);
    }

    public void UnlockRecipe(CraftRecipeSO blueprint)
    {
        if (UnlockedRecipes.ContainsKey(blueprint))
        {
            UnlockedRecipes[blueprint] = true;
        }
    }

    public void LockRecipe(CraftRecipeSO blueprint)
    {
        if (UnlockedRecipes.ContainsKey(blueprint))
        {
            UnlockedRecipes[blueprint] = false;
        }
    }

    [ContextMenu("Unlock All")]
    public void UnlockAllRecipes()
    {
        foreach (var kvp in UnlockedRecipes)
        {
            UnlockRecipe(kvp.Key);
        }
    }

    [ContextMenu("Lock All")]
    public void LockAllRecipes()
    {
        foreach (var kvp in UnlockedRecipes)
        {
            LockRecipe(kvp.Key);
        }
    }
}