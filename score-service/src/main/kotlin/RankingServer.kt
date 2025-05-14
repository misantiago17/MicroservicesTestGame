package com.example

import io.ktor.http.HttpStatusCode
import io.ktor.server.application.install
import io.ktor.server.engine.embeddedServer
import io.ktor.server.netty.Netty
import io.ktor.server.plugins.contentnegotiation.ContentNegotiation
import io.ktor.server.response.respond
import io.ktor.server.routing.get
import io.ktor.server.routing.routing
import io.ktor.serialization.kotlinx.json.*
import kotlinx.serialization.Serializable
import kotlinx.serialization.json.Json

fun main() {
    embeddedServer(Netty, port = 8081) {
        install(ContentNegotiation) {
            json(Json {
                prettyPrint = true
                isLenient = true
                ignoreUnknownKeys = true
            })
        }
        routing {
            get("/ranking") {
                val ranking = listOf(
                    Player("Michelle", 1500),
                    Player("Alex", 1200),
                    Player("João", 1000),
                    Player("Ryu", 800)
                )
                call.respond(ranking)
            }
        }
    }.start(wait = true)
}

@Serializable
data class Player(val player: String, val points: Int)