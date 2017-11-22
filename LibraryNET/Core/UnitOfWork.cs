namespace LibraryNET.Core
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    public sealed class UnitOfWork
	{
		/// <summary>
		/// Repropage l'exception ou pas (par défault l'exception est reporpagée)
		/// </summary>
		/// <value>
		///   <c>true</c> if [re throw exception]; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>Par défaut la valeur est à vrai</remarks>
		public bool ReThrowException { get; set; }

		/// <summary>
		/// Code à execute si la méthode se termine avec succès
		/// </summary>
		public Action OnSuccess { get; set; }

		/// <summary>
		/// Code à executer dans tous les cas
		/// </summary>
		public Action OnFinally { get; set; }

		/// <summary>
		/// Code à executer en cas d'exception
		/// </summary>
		public Action<Exception> OnFailure { get; set; }

		/// <summary>
		/// Récupération de l'exception générée
		/// </summary>
		/// <value>
		/// Exception générée.
		/// </value>
		public Exception Exception { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
		/// </summary>
		/// <remarks>
		/// la propriété ReThrowException est à vrai
		/// </remarks>
		public UnitOfWork()
		{
			this.ReThrowException = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
		/// </summary>
		/// <param name="onFinally">The on finally.</param>
		public UnitOfWork(Action onFinally)
			: this()
		{
			Contract.Requires<ArgumentNullException>(onFinally != null);

			this.OnFinally = onFinally;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
		/// </summary>
		/// <param name="onFinally">The on finally.</param>
		/// <param name="onFailure">The on failure.</param>
		public UnitOfWork(Action onFinally, Action<Exception> onFailure)
			: this(onFinally)
		{
			Contract.Requires<ArgumentNullException>(onFailure != null);

			this.OnFailure = onFailure;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
		/// </summary>
		/// <param name="onFinally">The on finally.</param>
		/// <param name="onFailure">The on failure.</param>
		/// <param name="onSuccess">The on success.</param>
		public UnitOfWork(Action onFinally, Action<Exception> onFailure, Action onSuccess)
			: this(onFinally, onFailure)
		{
			Contract.Requires<ArgumentNullException>(onSuccess != null);

			this.OnSuccess = onSuccess;
		}


		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", MessageId = "0")]
		public void Do(Action<UnitOfWork> action)
		{
			Contract.Requires<ArgumentNullException>(action != null);
			try
			{
				action(this);

				if (this.OnSuccess != null)
				{
					this.OnSuccess();
				}
			}
			catch (Exception e)
			{
				Trace.TraceError(e.Message);
				this.Exception = e;
				if (this.ReThrowException)
				{
					throw;
				}
			}
			finally
			{
				if (this.Exception != null)
				{
					if (this.OnFailure != null)
					{
						this.OnFailure(this.Exception);
					}
				}
				if (this.OnFinally != null)
				{
					this.OnFinally();
				}
			}
		}
	}
}
