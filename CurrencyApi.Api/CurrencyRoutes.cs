namespace currency_api
{
  public static class ExchangeRoutes
  {
    public const string DATA_POINT_GET =
    "/exchange/{datePoint:DateTime}";
    public const string DATA_RANGE_GET =
    "/exchange/{startDate:DateTime}/{endDate:DateTime}";

    public const string CURRENCY_FILTER =
    "/currency/{currency}";

    public const string AVG =
    "/avg";

    public const string DATA_POINT_ONE_CURRENCY_GET =
    DATA_POINT_GET + CURRENCY_FILTER;
    public const string DATA_RANGE_ONE_CURRENCY_GET =
    DATA_RANGE_GET + CURRENCY_FILTER;

    public const string DATA_RANGE_ONE_CURRENCY_AVG_GET =
    DATA_RANGE_ONE_CURRENCY_GET + AVG;

  }
}