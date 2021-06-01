import React, { FC } from 'react';
import { useToggle } from 'ahooks';
import { Avatar, Dropdown, Layout as AntLayout, Menu } from 'antd';
import {
  MenuUnfoldOutlined,
  MenuFoldOutlined,
  UserOutlined,
  MoreOutlined,
  HomeOutlined,
  ApartmentOutlined,
  ToolOutlined,
  DollarOutlined
} from '@ant-design/icons';
import { IProps } from './types';
import { useStyles } from './styles';
import { Typography } from 'antd';
import { AuthService } from '../../services';
import { useHistory } from 'react-router';
import { RouterPaths } from '../../consts';

const { Title, Text } = Typography;
const { Header, Sider, Content } = AntLayout;

const Layout: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const history = useHistory();
  const user = AuthService.User;

  const [collapsed, { toggle }] = useToggle(false);

  const onSignOut = () => {
    AuthService.SignOut();
    history.push(RouterPaths.Login);
  }

  const profileMenu = (
    <Menu>
      <Menu.Item>
        Profile
      </Menu.Item>
      <Menu.Item onClick={onSignOut}>
        Sign Out
      </Menu.Item>
    </Menu>
  );
  
    return (
      <AntLayout className={classes.layoutContainer}>
        <Sider trigger={null} collapsible collapsed={collapsed}>
          <div className={classes.logo}>
          <Title level={3}>{!collapsed ? "Loquimini" : "L"}</Title>
          </div>
          <Menu theme="dark" mode="inline">
            {AuthService.IsAdmin ? (
              <>
                <Menu.Item key="1" icon={<ApartmentOutlined />} onClick={() => history.push(RouterPaths.HouseList)}>
                  Houses
                </Menu.Item>
                <Menu.Item key="2" icon={<UserOutlined />} onClick={() => history.push(RouterPaths.UserList)}>
                  Users
                </Menu.Item>
                <Menu.Item key="3" icon={<ToolOutlined />} onClick={() => history.push(RouterPaths.Triggers)}>
                  Triggers
                </Menu.Item>
              </>
            ) : (
              <>
                <Menu.Item key="1" icon={<HomeOutlined />} onClick={() => history.push(RouterPaths.Dashboard)}>
                  Dashboard
                </Menu.Item>
                <Menu.Item key="2" icon={<DollarOutlined />} onClick={() => history.push(RouterPaths.Receipts)}>
                  Receipts
                </Menu.Item>
              </>
            )}
          </Menu>
        </Sider>
        <AntLayout className={classes.layout}>
          <Header className={classes.header}>
            {collapsed ? 
              <MenuUnfoldOutlined className={classes.trigger} onClick={() => toggle()}/> 
              : 
              <MenuFoldOutlined  className={classes.trigger} onClick={() => toggle()}/>
            }
            <div className={classes.profile}>
              <Avatar className={classes.user} icon={<UserOutlined />}/>
              <Text >{`${user.firstName} ${user.lastName}`.trim()}</Text>
              <Dropdown overlay={profileMenu} trigger={['click']}>
                <MoreOutlined />
              </Dropdown>
            </div>
          </Header>
          <Content
            className={classes.contentContainer}
          >
            <div className={classes.content}>
              {props.children}
            </div>
          </Content>
        </AntLayout>
      </AntLayout>
    );
};

export default Layout;