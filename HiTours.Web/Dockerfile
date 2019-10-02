#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-nanoserver-1809 AS build
WORKDIR /src
COPY ["HiTours.Web/HiTours.Web.csproj", "HiTours.Web/"]
COPY ["Razorpay/Razorpay.csproj", "Razorpay/"]
COPY ["HiTours.ViewModels/HiTours.ViewModels.csproj", "HiTours.ViewModels/"]
COPY ["HiTours.Data/HiTours.Data.csproj", "HiTours.Data/"]
COPY ["HiTours.Core/HiTours.Core.csproj", "HiTours.Core/"]
COPY ["HiTours.TBO.Models/HiTours.TBO.Models.csproj", "HiTours.TBO.Models/"]
COPY ["Common/HiTours.Api.Common.Caching/HiTours.Api.Common.Caching.csproj", "Common/HiTours.Api.Common.Caching/"]
COPY ["Common/HiTours.Api.Common/HiTours.Api.Common.csproj", "Common/HiTours.Api.Common/"]
COPY ["HiTours.Services/HiTours.Services.csproj", "HiTours.Services/"]
COPY ["Common/HiTours.Api.Common.Caching.Repository/HiTours.Api.Common.Caching.Repository.csproj", "Common/HiTours.Api.Common.Caching.Repository/"]
COPY ["Common/HiTours.Api.Common.Data/HiTours.Api.Common.Data.csproj", "Common/HiTours.Api.Common.Data/"]
COPY ["Configuration/HiTours.Api.Configuration.Business/HiTours.Api.Configuration.Business.csproj", "Configuration/HiTours.Api.Configuration.Business/"]
COPY ["Configuration/HiTours.Api.Configuration.Contract/HiTours.Api.Configuration.Contract.csproj", "Configuration/HiTours.Api.Configuration.Contract/"]
COPY ["Common/HiTours.Api.Common.Contract/HiTours.Api.Common.Contract.csproj", "Common/HiTours.Api.Common.Contract/"]
RUN dotnet restore "HiTours.Web/HiTours.Web.csproj"
COPY . .
WORKDIR "/src/HiTours.Web"
RUN dotnet build "HiTours.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HiTours.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HiTours.Web.dll"]