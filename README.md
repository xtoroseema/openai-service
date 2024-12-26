# OpenAI gRPC Microservice

This project implements a gRPC-based microservice that interacts with OpenAI's API to provide AI-powered functionalities like text generation, code completion, or any other supported OpenAI service. It is designed for efficient communication and scalability.

---

## Features

- **gRPC API** for high-performance, low-latency communication.
- Supports OpenAI endpoints for text generation and more.
- Configurable via environment variables for secure and flexible deployment.

---

## Prerequisites

### Requirements

- OpenAI API Key
- OpenAI assistant ID

### Technologies Used

- **Language**: C#
- **Framework**: gRPC/ASP NET
- **External API**: OpenAI

---

## Setup

### Clone the Repository
```bash
git clone https://github.com/yourusername/openai-grpc-microservice.git
cd openai-service
```

## gRPC API

### Service Definition
The gRPC service is defined in the `message.proto` file.

```proto
syntax = "proto3";

option csharp_namespace = "OpenAIGRPC";

message MessageRequest
{
  string message = 1;
}

message MessageResponse
{
  string message = 1;
}

service ResponseService
{
  rpc GenerateResponse (MessageRequest) returns (MessageResponse);
}
```

---

## Security

- Use environment variables to store sensitive information like API keys.
- Avoid hardcoding credentials in the source code.

---

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

---

## Contact

For any questions or issues, contact [yourname](t.me/xtoroseema).

