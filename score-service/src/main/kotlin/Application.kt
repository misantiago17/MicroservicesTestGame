package com.example

import com.fasterxml.jackson.module.kotlin.jsonMapper
import io.ktor.http.HttpStatusCode
import io.ktor.server.engine.*
import io.ktor.server.netty.*
import io.ktor.server.application.*
import io.ktor.server.response.*
import io.ktor.server.request.*
import io.ktor.server.routing.*
import io.ktor.server.plugins.contentnegotiation.*
import kotlinx.serialization.Serializable

fun main() {
    embeddedServer(Netty, port = 8080, host = "0.0.0.0") {
        install(ContentNegotiation) {
            jsonMapper()
        }
    }.start(wait = true)
}

@Serializable
data class StatusResponse(val message: String)

@Serializable
data class Score (
    val player: String,
    val points: Int
)

fun Application.module() {
    configureSerialization()
    configureMonitoring()
    configureRouting()
}
