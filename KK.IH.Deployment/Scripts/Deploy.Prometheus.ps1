## Uninstall previous release
helm uninstall prometheus

## Package chart
helm package ./../Helm/prometheus

## Install realse
helm upgrade --install prometheus prometheus-0.1.0.tgz