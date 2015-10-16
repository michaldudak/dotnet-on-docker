FROM microsoft/aspnet

ADD . /app
WORKDIR /app

RUN dnu restore

EXPOSE 50000

ENTRYPOINT ["dnx", "web"]