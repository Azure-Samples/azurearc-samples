// This is a go code sample that counts from 0 to inifnite
package main

import (
    "fmt"
    "time"
)

func main() {
    for i := 0; true; i++ {
        fmt.Println(i)
        time.Sleep(time.Second)
    }
}