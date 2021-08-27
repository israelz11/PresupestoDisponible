using System.Web;

namespace PresupDisponible.Models.Security
{
    public static class SessionHelpers
    {
        #region Enumerators

        private enum Keys
        {
            IdPerson,
            UserName,
            IdApplication,
            IdDependency,
            Email,
            AppName
        }

        #endregion

        #region Methods

        #region Person Methods 

        public static int IdPerson(this HttpSessionStateBase session)
        {
            return session[Keys.IdPerson.ToString()] == null ? 0 : (int)session[Keys.IdPerson.ToString()];
        }

        public static string AppName(this HttpSessionStateBase session)
        {
            return session[Keys.AppName.ToString()] == null ? "" : session[Keys.AppName.ToString()].ToString();
        }

        public static string UserName(this HttpSessionStateBase session)
        {
            return session[Keys.UserName.ToString()] == null ? "" : session[Keys.UserName.ToString()].ToString();
        }

        public static string Email(this HttpSessionStateBase session)
        {
            return session[Keys.Email.ToString()] == null ? "" : session[Keys.Email.ToString()].ToString();
        }

        public static int IdApplication(this HttpSessionStateBase session)
        {
            return session[Keys.IdApplication.ToString()] == null ? 0 : int.Parse(session[Keys.IdApplication.ToString()].ToString());
        }

        public static int IdDependency(this HttpSessionStateBase session)
        {
            return session[Keys.IdDependency.ToString()] == null ? 0 : int.Parse(session[Keys.IdDependency.ToString()].ToString());
        }

        public static void SetIdPerson(this HttpSessionStateBase session, int IdPerson)
        {
            session[Keys.IdPerson.ToString()] = IdPerson;
        }

        public static void SetAppName(this HttpSessionStateBase session, string AppName)
        {
            session[Keys.AppName.ToString()] = AppName;
        }

        public static void SetUserName(this HttpSessionStateBase session, string UserName)
        {
            session[Keys.UserName.ToString()] = UserName;
        }

        public static void SetEmail(this HttpSessionStateBase session, string Email)
        {
            session[Keys.Email.ToString()] = Email;
        }

        public static void SetIdApplication(this HttpSessionStateBase session, int IdApplication)
        {
            session[Keys.IdApplication.ToString()] = IdApplication;
        }

        public static void SetIdDependency(this HttpSessionStateBase session, int IdDependency)
        {
            session[Keys.IdDependency.ToString()] = IdDependency;
        }

        #endregion

        #region Others

        public static int IdLog(this HttpSessionStateBase session)
        {
            return session["IdLog"] == null ? 0 : int.Parse(session["IdLog"].ToString());
        }

        public static void SetIdLog(this HttpSessionStateBase session, int IdLog)
        {
            session["IdLog"] = IdLog;
        }

        #endregion

        #endregion

    }
}