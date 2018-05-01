﻿using System;

namespace NWheels.CodeGeneration.CodeModel.Members
{
    public struct TypeGeneratorInfo
    {
        public TypeGeneratorInfo(Type factoryType)
        {
            this.FactoryType = factoryType;
            this.TypeKey = null;
            this.ActivationContract = null;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public TypeGeneratorInfo(Type factoryType, TypeKey typeKey, Type activationContract = null)
        {
            this.FactoryType = factoryType;
            this.TypeKey = typeKey;
            this.ActivationContract = activationContract ?? typeof(object);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public Type FactoryType { get; }
        public TypeKey? TypeKey { get; }

        // return type of factory interface
        public Type ActivationContract { get; }
    }
}
