using System;
using BaselineSolution.Bo.Models.Shared;

namespace BaselineSolution.WebApp.Infrastructure.Models.Authentication.SessionModels
{
    [Serializable]
    public class AccountCultureSessionModel
    {
        public int Id { get; set; }
        public string Culture { get; set; }

        public AccountCultureSessionModel(SystemLanguageBo accountCulture)
        {
            Id = accountCulture.Id;
            Culture = accountCulture.Culture;
        }
    }
}