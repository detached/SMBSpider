NFSLibrary 0.7

mirko.gatto@gmail.com

- This new version embeds remotetea.net. You need to deploy only this library.

- On Ubuntu server if you receive mount error you have to include in the export
the uid and gid. In the following example the client gid and uid are 1000:

/export 192.168.0.14(rw,no_subtree_check,sync,insecure,anonuid=1000,anongid=1000)

