﻿namespace BTPTC.Service.Interface
{
    public interface IUtilityService
    {
        string SendEmail(string Subject, string Content, string To, bool ISBCC, string FromEmail, string CCEmail = "");
    }
}
