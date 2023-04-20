using System;

public class ExpressionObj
{
    public string variableName;
    public string atlasName;
    public string desc;
    public string expression;

    public bool Invalid => CheckInvalid();

    private bool CheckInvalid()
    {
        if (string.IsNullOrEmpty(variableName)
            || string.IsNullOrEmpty(atlasName))
            return true;
        else return false;
    }
}
