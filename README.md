# Authentication and Authorization

## Creating the secret for JWT
Create user secret by right clicking on the project and then selecting 'Manage User Secrets'. Then structure the json
content as follow:
```
{
  "JwtSecurityToken": {
    "Key": "<enter a base64 encoded key here. can be generated from git bash using command: 'openssl rand -base64 32'"
  }
}
```
