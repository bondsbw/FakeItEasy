namespace FakeItEasy.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using FakeItEasy.Core;

    /// <summary>
    /// A fake implementation of IFakeObjectCall, used for testing.
    /// </summary>
    public class FakeCall : IInterceptedFakeObjectCall, ICompletedFakeObjectCall
    {
        public FakeCall()
        {
            this.Arguments = ArgumentCollection.Empty;
        }

        public MethodInfo Method { get; set; }

        public ArgumentCollection Arguments { get; set; }

        public object ReturnValue { get; private set; }

        public object FakedObject { get; set; }

        public string Description
        {
            get { return this.ToString(); }
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "By design.")]
        public static FakeCall Create<T>(string methodName, Type[] parameterTypes, object[] arguments) where T : class
        {
            var method = typeof(T).GetMethod(methodName, parameterTypes);

            return new FakeCall
            {
                Method = method,
                Arguments = new ArgumentCollection(arguments, method),
                FakedObject = A.Fake<T>()
            };
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "By design.")]
        public static FakeCall Create<T>(string methodName) where T : class
        {
            return Create<T>(methodName, new Type[] { }, new object[] { });
        }

        public void SetReturnValue(object value)
        {
            this.ReturnValue = value;
        }

        public ICompletedFakeObjectCall AsReadOnly()
        {
            return this;
        }

        public void CallBaseMethod()
        {
        }

        public void SetArgumentValue(int index, object value)
        {
        }

        public void DoNotRecordCall()
        {
        }
    }
}