## Uninstall previous release
helm uninstall grafana --wait

## Package chart
helm package ./../Helm/grafana

## Install realse
helm upgrade --install grafana grafana-0.1.0.tgz
