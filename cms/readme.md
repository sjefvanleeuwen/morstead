# ![logo](../images/logo.svg) Virtual Society

## CMS

### Storage Providers

#### GitHub File Storage Service

Virtual Society biedt via deze service de mogelijkheid om bestand onder versiebeheer op te slaan op Github.

##### Configureren

Voor Unit Tests project voeg een bestand toe genaamd: cred.json met de volgende gegevens:

```json
{
  "github": {
    "api-key": "<jouw api key>",
    "repo": "<naam van de repository>",
    "user": "<naam van de gebruiker>",
    "product": "<naam van het product>" 
  }
}
```

Voor een uitgebreide beschrijving hoe tokens te configureren zie: https://github.com/settings/tokens