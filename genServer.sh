#!/bin/bash

#java -jar /{path_to_swagger_codegen}/modules/swagger-codegen-cli/target/swagger-codegen-cli.jar generate -i /{path_to_swagger.yaml} -l {language} -o /{path_to_save}


echo "Create NodeJs Server for swagger UI"
java -jar /home/ec2-user/swagger-codegen/modules/swagger-codegen-cli/target/swagger-codegen-cli.jar generate -i /home/ec2-user/swagger-codegen/swagger.yaml -l nodejs-server -o /home/ec2-user/node_server
echo "Done"