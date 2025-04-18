namespace MyAspNetApp.Utils.Template
{
    public static class PaymentSuccessTemplate
    {
        public static string Generate(string fullName, string orderCode, decimal amount, DateTime paidAt)
        {
            return $@"
                <div style='
                    margin: 0;
                    font-family: ""Poppins"", sans-serif;
                    background: #ffffff;
                    font-size: 14px;
                '>
                    <div style='
                        max-width: 680px;
                        margin: 0 auto;
                        padding: 45px 30px 60px;
                        background: #f4f7ff;
                        background-repeat: no-repeat;
                        background-size: 800px 452px;
                        background-position: top center;
                        font-size: 14px;
                        color: #434343;
                    '>
                        <main>
                            <div style='
                                margin: 0;
                                margin-top: 70px;
                                padding: 92px 30px 115px;
                                background: #ffffff;
                                border-radius: 30px;
                                text-align: center;
                            '>
                                <div style='width: 100%; max-width: 489px; margin: 0 auto;'>
                                    <h1 style='
                                        margin: 0;
                                        font-size: 24px;
                                        font-weight: 600;
                                        color: #1f1f1f;
                                    '>
                                        Thanh toán thành công!
                                    </h1>
                                    <p style='
                                        margin-top: 20px;
                                        font-size: 16px;
                                    '>
                                        Chào <strong>{fullName}</strong>, đơn hàng <strong>{orderCode}</strong> của bạn đã được thanh toán thành công với số tiền <strong>{amount:C}</strong>.
                                    </p>
                                    <p style='margin-top: 10px;'>
                                        Thời gian thanh toán: <strong>{paidAt.ToString("HH:mm dd/MM/yyyy")}</strong>
                                    </p>
                                    <p style='margin-top: 30px; color: #555555;'>
                                        Cảm ơn bạn đã mua sắm tại cửa hàng của chúng tôi!
                                    </p>
                                </div>
                            </div>
                        </main>
                    </div>
                </div>
            ";
        }
    }
}
