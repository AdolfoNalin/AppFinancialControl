using AppFinancialControl.Service;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace AppFinancialControl
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            #if DEBUG
    		builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }

        #region RegisterDatabaseAndRepository
        public static MauiAppBuilder RegisterDatabaseAndRepository(MauiAppBuilder mauiAppBuilder)
        {
            try
            {
                mauiAppBuilder.Services.AddSingleton<LiteDatabase>(
                    options =>
                    new LiteDatabase($"{AppSettings.DataBasePath};Connection=shared")
                    );

                mauiAppBuilder.Services.AddTransient<ITransactionService, TransactionService>();

                return mauiAppBuilder;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
