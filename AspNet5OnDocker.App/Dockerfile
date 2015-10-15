FROM microsoft/aspnet

RUN mkdir ~/app
WORKDIR ~/app
ADD . .

RUN dnu restore

EXPOSE 50000

ENTRYPOINT ["dnx", "web"]