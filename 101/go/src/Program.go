// This is a go code sample that counts from 1 to 100
package main

import (
    "fmt"
    "time"
)

func main() {
    for i := 0; i <= 100; i++ {
        fmt.Println(i)
        time.Sleep(time.Second)
    }
}