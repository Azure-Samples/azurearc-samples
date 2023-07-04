# This is a possible Dockerfile for the go code sample that counts from 1 to 100
FROM golang:1.17-alpine
WORKDIR /src
COPY Program.go .
RUN go build Program.go
CMD ["./Program"] 