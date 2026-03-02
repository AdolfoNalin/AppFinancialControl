using AppFinancialControl.Service;
using AppFinancialControl.View;
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
                })
                .RegisterDatabaseAndRepository();

            #if DEBUG
    		builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }

        #region RegisterDatabaseAndRepository
        public static MauiAppBuilder RegisterDatabaseAndRepository(this MauiAppBuilder mauiAppBuilder)
        {
            try
            {
                mauiAppBuilder.Services.AddSingleton<LiteDatabase>(
                    options =>
                        {
                            return new LiteDatabase($"Filename={AppSettings.DataBasePath};Connection=shared");
                        });

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
