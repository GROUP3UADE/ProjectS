using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Tests
{
    internal class FactorySO
    {
        #region SetUp

        BlockMapSOFactory _blockSOFactory;
        BlockMapNodeFactory _blockNodeFactory;
        BuildingMapSOFactory _buildingSOFactory;
        BuildingMapNodeFactory _buildingNodeFactory;
        BlockMapNodeSO _standarBlock;
        BlockMapNodeSO _emptyBlock;
        BlockMapNodeSO _fullBlock;
        BuildingMapNodeSO _standarBuilding;
        BuildingMapNodeSO _importantBuilding;
        BuildingMapNodeSO _keyBuilding;


        public FactorySO()
        {
            _blockSOFactory = new BlockMapSOFactory();
            _blockNodeFactory = new BlockMapNodeFactory();
            _buildingSOFactory = new BuildingMapSOFactory();
            _buildingNodeFactory = new BuildingMapNodeFactory();

            _standarBlock = GetStandarBlockSO();
            _emptyBlock = GetEmptyStandarBlock();
            _fullBlock = GetFullStandarBlock();

            _standarBuilding = GetStandarBuildingSO(3, 9);
            _importantBuilding = GetImportantBuildingSO();
            _keyBuilding = GetKeyBuildingSO();
        }

        #endregion

        public City GetCity(int oneSlotBlockAmmount = 5, int emptyBlockAmmount = 5, int fullBlockAmmount = 10)
        {
            City c = new City();
            c.Blocks = new List<Block>();

            for (int i = 0; i < oneSlotBlockAmmount; i++) c.Blocks.Add(GetBlock(_standarBlock));
            for (int i = 0; i < emptyBlockAmmount; i++) c.Blocks.Add(GetBlock(_emptyBlock));
            for (int i = 0; i < fullBlockAmmount; i++) c.Blocks.Add(GetBlock(_fullBlock));

            return c;
        }

        #region Blocks

        public BlockMapNodeSO GetStandarBlockSO(int children = 3) => _blockSOFactory.Create(children, 2, 2);
        public BlockMapNodeSO GetEmptyStandarBlock() => _blockSOFactory.Create(0, 2, 2);
        public BlockMapNodeSO GetFullStandarBlock() => _blockSOFactory.Create(4, 2, 2);
        public Block GetBlock(BlockMapNodeSO so) => _blockNodeFactory.Create(so);

        #endregion

        #region Buildings

        private BuildingMapNodeSO GetStandarBuildingSO(int amountPerCity, int amountPerWorld) => _buildingSOFactory.Create(amountPerCity, amountPerWorld, false, 1, 1);
        private BuildingMapNodeSO GetKeyBuildingSO() => _buildingSOFactory.Create(1, 1, false, 1, 1);
        private BuildingMapNodeSO GetImportantBuildingSO() => _buildingSOFactory.Create(3, 1, false, 1, 1);
        private Building GetBuilding(BuildingMapNodeSO so) => _buildingNodeFactory.Create(so);

        #endregion
    }

    internal class BlockMapSOFactory : BlockMapNodeSO
    {
        public BlockMapNodeSO Create(int children, int width, int height) =>
            new BlockMapSOFactory()
            {
                _children = children,
                _width = width,
                _height = height

            } as BlockMapNodeSO;
    }
    internal class BuildingMapSOFactory : BuildingMapNodeSO
    {
        public BuildingMapNodeSO Create(int amountPerCity, int amountPerWorld, bool isKeyBuilding, int heightOnBlock, int widthOnBlock) =>
            new BuildingMapSOFactory()
            {
                _amountPerCity = amountPerCity,
                _amountPerWorld = amountPerWorld,
                _isKeyBuilding = isKeyBuilding,
                _heightOnBlock = heightOnBlock,
                _widthOnBlock = widthOnBlock
            } as BuildingMapNodeSO;
        
    }
    internal class BlockMapNodeFactory : Block
    {
        public Block Create(BlockMapNodeSO so) => new BlockMapNodeFactory() { Settings = so };
            
    }
    internal class BuildingMapNodeFactory : Building
    {
        public Building Create(BuildingMapNodeSO so) => new BuildingMapNodeFactory() { Settings = so };
    }
}
