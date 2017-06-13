using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.WebApp.Infrastructure.Models.Authentication.SessionModels;

namespace BaselineSolution.WebApp.Infrastructure.Models.Authentication
{
    public class AnonymousCustomPrincipal : ICustomPrincipal
    {
        private const string Message = "The user is not logged in!";

        public AnonymousCustomPrincipal()
        {
            Identity = new AnonymousIdentity();
        }

        public bool IsInRole(string role)
        {
            throw new InvalidOperationException(Message);
        }

        public IIdentity Identity { get; private set; }

        public int Id
        {
            get { throw new InvalidOperationException(Message); }
        }

        public AccountSessionModel CurrentAccount
        {
            get { throw new InvalidOperationException(Message); }
            set { throw new InvalidOperationException(Message); }
        }

        public AccountSessionModel MainAccount
        {
            get { throw new InvalidOperationException(Message); }
        }



        public int? OwnerId
        {
            get { throw new InvalidOperationException(Message); }
        }

        public string Username
        {
            get { throw new InvalidOperationException(Message); }
        }

        public IEnumerable<string> Roles
        {
            get { throw new InvalidOperationException(Message); }
        }

        public IEnumerable<int> RoleIds
        {
            get { throw new InvalidOperationException(Message); }
        }

        public IEnumerable<int> AllowedRoleIds
        {
            get { throw new InvalidOperationException(Message); }
        }

        public bool IsAdministrator
        {
            get { throw new InvalidOperationException(Message); }
        }

        public string DefaultCulture
        {
            get
            {
                throw new InvalidOperationException(Message);
            }
        }

        public bool Owns(IOwnable ownable)
        {
            throw new InvalidOperationException(Message);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}