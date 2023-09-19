FROM golang:1.21-alpine
ENV AZURE_TENANT_ID="<TenantId>"
ENV AZURE_CLIENT_ID="<ClientId>"
ENV AZURE_CLIENT_SECRET="<ClientSecret>"
WORKDIR /src
COPY * .
RUN go mod download
RUN go build Program.go
CMD ["./Program"] 