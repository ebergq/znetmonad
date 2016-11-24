VAGRANTFILE_API_VERSION = "2"

Vagrant.configure(VAGRANTFILE_API_VERSION) do |config|
  config.vm.define "presentation" do |dotnet|
    dotnet.vm.box = "ubuntu/xenial64"
    dotnet.vm.network "private_network", ip: "192.168.33.20"
    dotnet.vm.provider "virtualbox" do |v|
      v.memory = 2048
      v.cpus = 2
    end
    dotnet.vm.provision :shell, path: "bootstrap.sh"
  end
end
