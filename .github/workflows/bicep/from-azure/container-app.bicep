param containerapps_ca_dev_tag_customer_name string = 'ca-dev-tag-customer'
param managedEnvironments_caenv_dev_tag_private_externalid string = '/subscriptions/b435bfd7-28d3-4016-955b-baf44b31c6b5/resourceGroups/rg-dev-tag/providers/Microsoft.App/managedEnvironments/caenv-dev-tag-private'

resource container_app 'Microsoft.App/containerApps@2026-01-01' = {
  name: containerapps_ca_dev_tag_customer_name
  location: 'uksouth'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    managedEnvironmentId: managedEnvironments_caenv_dev_tag_private_externalid
    environmentId: managedEnvironments_caenv_dev_tag_private_externalid
    workloadProfileName: 'Consumption'
    configuration: {
      secrets: [
        {
          name: 'reg-pswd-21f0b2eb-bbc1'
        }
        {
          name: 'reg-pswd-4c465114-9f2b'
        }
      ]
      activeRevisionsMode: 'Single'
      ingress: {
        external: true
        targetPort: 8080
        exposedPort: 0
        transport: 'Auto'
        traffic: [
          {
            weight: 100
            latestRevision: true
          }
        ]
        allowInsecure: false
        clientCertificateMode: 'Ignore'
        stickySessions: {
          affinity: 'none'
        }
      }
      registries: [
        {
          server: 'ghcr.io'
          username: 'mattyfb2'
          passwordSecretRef: 'reg-pswd-4c465114-9f2b'
        }
      ]
      identitySettings: []
      maxInactiveRevisions: 100
    }
    template: {
      containers: [
        {
          image: 'ghcr.io/sming-code/demo-customer-svc-api:1.0.33'
          imageType: 'ContainerImage'
          name: containerapps_ca_dev_tag_customer_name
          env: [
            {
              name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
              value: 'InstrumentationKey=6b355961-0a59-4b77-8afe-c765f0207fa1;IngestionEndpoint=https://uksouth-1.in.applicationinsights.azure.com/;LiveEndpoint=https://uksouth.livediagnostics.monitor.azure.com/;ApplicationId=537f96be-bd90-4832-aa45-6ff8577ce0d4'
            }
            {
              name: 'Service_Name'
              value: 'Customer Service'
            }
            {
              name: 'Database__ConnectionString'
              value: 'Server=tcp:dev-tag.database.windows.net,1433;Initial Catalog=sql-dev-tag-private-customer;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication="Active Directory Default";'
            }
            {
              name: 'Kafka__BootstrapServers'
              value: 'ca-dev-tag-private-kafka-0:29092'
            }
            {
              name: 'Kafka__SecurityProtocol'
              value: 'Plaintext'
            }
            {
              name: 'Logging__LogLevel__Default'
              value: 'Trace'
            }
          ]
          resources: {
            cpu: json('0.5')
            memory: '1Gi'
          }
          probes: [
            {
              type: 'Liveness'
              failureThreshold: 3
              periodSeconds: 10
              successThreshold: 1
              tcpSocket: {
                port: 8080
              }
              timeoutSeconds: 5
            }
            {
              type: 'Readiness'
              failureThreshold: 48
              periodSeconds: 5
              successThreshold: 1
              tcpSocket: {
                port: 8080
              }
              timeoutSeconds: 5
            }
            {
              type: 'Startup'
              failureThreshold: 240
              initialDelaySeconds: 1
              periodSeconds: 1
              successThreshold: 1
              tcpSocket: {
                port: 8080
              }
              timeoutSeconds: 3
            }
          ]
          volumeMounts: [
            {
              volumeName: 'env-storage'
              mountPath: '/data'
              subPath: 'env-data/customer-svc'
            }
          ]
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 10
        cooldownPeriod: 300
        pollingInterval: 30
        rules: [
          {
            name: 'http-scaler'
            http: {
              metadata: {
                concurrentRequests: '10'
              }
            }
          }
        ]
      }
      volumes: [
        {
          name: 'env-storage'
          storageType: 'AzureFile'
          storageName: 'smb-fs-dev-tag-caenv-private'
          mountOptions: 'dir_mode=0777,file_mode=0777,mfsymlinks,cache=strict,nosharesock,nobrl'
        }
      ]
    }
  }
}
