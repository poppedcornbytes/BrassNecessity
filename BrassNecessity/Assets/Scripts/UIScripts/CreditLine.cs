using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditLine
{
    private const string GAME_TITLE_STYLE = "credit-title";
    private const string CREDIT_HEADER_STYLE = "credit-header";
    private const string CREDIT_SUBHEADER_STYLE = "credit-subheader";
    private const string CREDIT_ROLENAME_STYLE = "credit-name-and-role";
    private Label creditLineLabel;
    public Label CreditLineLabel { get => creditLineLabel; }
    private VisualElement containerElement;
    public CreditLine(string creditInfo, VisualElement containerElement)
    {
        this.containerElement = containerElement;
        initialiseLabel(creditInfo);
    }

    private void initialiseLabel(string creditInfo)
    {
        creditLineLabel = new Label();
        creditLineLabel.text = removeStringPrefix(creditInfo);
        containerElement.Add(creditLineLabel);
        string style = getCreditStyleClass(creditInfo);
        creditLineLabel.ToggleInClassList(style);
        creditLineLabel.RegisterCallback<GeometryChangedEvent>(onGeometryChanged);
    }

    private string getCreditStyleClass(string creditLine)
    {
        int headerLevel = getPrefixCount(creditLine);
        string creditStyle;
        switch (headerLevel)
        {
            case 1:
                creditStyle = GAME_TITLE_STYLE;
                break;
            case 2:
                creditStyle = CREDIT_HEADER_STYLE;
                break;
            case 3:
                creditStyle = CREDIT_SUBHEADER_STYLE;
                break;
            default:
                creditStyle = CREDIT_ROLENAME_STYLE;
                break;
        }
        return creditStyle;
    }

    private int getPrefixCount(string creditLine)
    {
        int headerLevel = 0;
        if (creditLine.Length > 0)
        {
            int i = 0;
            char c;
            do
            {
                c = creditLine[i];
                if (c == '#')
                {
                    headerLevel++;
                }
                i++;
            } while (c == '#' && i < creditLine.Length);
        }
        return headerLevel;
    }

    private string removeStringPrefix(string creditLine)
    {
        string textWithoutPrefix = string.Empty;
        int alphaNumIndex = findFirstAlphaNumIndex(creditLine);
        if (alphaNumIndex >= 0)
        {
            textWithoutPrefix = creditLine.Substring(alphaNumIndex);
        }
        return textWithoutPrefix;
    }

    private int findFirstAlphaNumIndex(string stringToCheck)
    {
        int i = 0;
        bool found = false;
        int alphaNumIndex = -1;
        while (!found && i < stringToCheck.Length)
        {
            if (char.IsLetterOrDigit(stringToCheck[i]))
            {
                found = true;
                alphaNumIndex = i;
            }
            i++;
        }
        return alphaNumIndex;
    }
    
    private void onGeometryChanged(GeometryChangedEvent evt)
    {
        creditLineLabel.UnregisterCallback<GeometryChangedEvent>(onGeometryChanged);
        creditLineLabel.ToggleInClassList("scroll-up-animation");
    }

    public void DestroyLabel()
    {
        containerElement.Remove(creditLineLabel);
    }
}
