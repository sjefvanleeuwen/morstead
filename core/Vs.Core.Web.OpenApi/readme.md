Virtual Society Open API strategy uses JWT tokens with public/private key infrastructure (assymetric)

To generate a private key:

```
openssl genrsa -out jwtRS256.key 4096
```

To generate a public key:

```
openssl rsa -in jwtRS256.key -pubout -out jwtRS256.key.pub
```

For demo purposes, you can call the API service to sign a token. The needed claims are 
automatically discovered from the Authorize headers defined within the API controllers.

2 capabilities are available in the contact version \<version>\-core.

1. Claims discovery
2. Signing Token

**!NOTE** tokens are signed and not encrypted. use HTTPS for transport.



