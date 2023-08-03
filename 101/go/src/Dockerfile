# This is a Dockerfile for the go code sample that counts from 0 to infinite 
FROM golang:1.17-alpine
WORKDIR /src
COPY Program.go .
RUN go build Program.go
CMD ["./Program"] 